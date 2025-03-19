using DealUp.Domain.Email.Interfaces;
using DealUp.Domain.User;
using DealUp.Domain.User.Interfaces;
using DealUp.Exceptions;
using DealUp.Infrastructure.Configuration;
using DealUp.Utils;
using Microsoft.Extensions.Options;

namespace DealUp.Services.User;

public class UserService(IUserRepository userRepository, IEmailSendingService emailSendingService, IOptions<EmailSendingOptions> options) : IUserService
{
    public async Task<StartVerificationResponse> SendVerificationEmailIfNeededAsync(Guid userId)
    {
        var pendingConfirmation = await userRepository.GetPendingConfirmationAsync(userId, ConfirmationType.VerifyEmail);

        if (pendingConfirmation is null)
        {
            await SendEmailVerificationRequestAsync(userId);
            return StartVerificationResponse.CreateSuccessful("A verification email has been successfully sent.");
        }

        if (pendingConfirmation.IsRecent())
        {
            return StartVerificationResponse.CreateUnsuccessful("A verification email has already been sent recently. Please wait before requesting another one.");
        }

        pendingConfirmation.SetAsUsed();
        await userRepository.UpdatePendingConfirmationAsync(pendingConfirmation);

        await SendEmailVerificationRequestAsync(userId);
        return StartVerificationResponse.CreateSuccessful("A verification email has been successfully sent.");
    }

    public async Task VerifyUserAsync(FinishVerificationRequest request)
    {
        var pendingConfirmation = await userRepository.GetPendingConfirmationAsync(request.UserId, ConfirmationType.VerifyEmail);
        if (pendingConfirmation is null || pendingConfirmation.Token != request.Token)
        {
            throw new VerificationValidationException();
        }

        pendingConfirmation.SetAsUsed();
        await userRepository.UpdatePendingConfirmationAsync(pendingConfirmation);
        await userRepository.SetUserStatusAsync(request.UserId, UserVerificationStatus.Confirmed);
    }

    private async Task SendEmailVerificationRequestAsync(Guid userId)
    {
        var secureToken = CryptoUtils.GetRandomString(options.Value.SecureTokenLength);
        var pendingVerification = PendingConfirmation.CreateForEmailVerification(userId, secureToken);

        await userRepository.SaveNewPendingConfirmationAsync(pendingVerification);
        await emailSendingService.SendEmailVerificationAsync(pendingVerification);
    }
}