using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MyWebApi.Infrastructure.Repository___service;
using RazorLight;
using WebAPIProject.Contract.Repositories;
using WebAPIProject.Contract.Service;
using WebAPIProject.Core.DTOs;
using WebAPIProject.Core.Models.MyWebApi.Core.Model;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly IEmailRepository _emailRepository;
    private readonly INotificationService _notificationService;

    public EmailService(IConfiguration configuration, IEmailRepository emailRepository)
    {
        _configuration = configuration;
        _emailRepository = emailRepository;
    }

    public async Task<SendEmailResponse> SendEmail(int userId)
    {
        var response = new SendEmailResponse();

        try
        {
            // ✅ Fetch User from DB
            var user = await _emailRepository.GetUserById(userId);
            if (user == null)
            {
                response.IsSuccess = false;
                response.Message = "User not found.";
                return response;
            }

            var engine = new RazorLightEngineBuilder()
              .UseFileSystemProject(Path.Combine(Directory.GetCurrentDirectory(), "View"))
              .UseMemoryCachingProvider()
              .Build();


            var model = new SendEmailResponse
            {
                UserName = user.UserName,
                UserAdd = user.UserAdd,
                email = user.Email
            };

            // string templateKey = "Email/Email.cshtml";
            string emailBody = await engine.CompileRenderAsync("Email/Email.cshtml", model);


            // ✅ SMTP Configuration
            var smtpSettings = _configuration.GetSection("SmtpSettings");
            string host = smtpSettings["Host"];
            int port = int.Parse(smtpSettings["Port"]);
            bool enableSSL = bool.Parse(smtpSettings["EnableSSL"]);
            string username = smtpSettings["Username"];
            string fullname = smtpSettings["Fullname"];
            string password = smtpSettings["Password"];

            // ✅ Create Email
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(fullname, username));
            message.To.Add(new MailboxAddress(user.UserName, user.Email));
            message.Subject = "Welcome to Assimilate!";
            message.Body = new TextPart("html") { Text = emailBody };

            // ✅ Send Email
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(host, port, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(username, password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }

            response.IsSuccess = true;
            response.Message = "Email sent successfully!";
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = $"Error: {ex.Message}";
        }
        await _notificationService.CreateNotificationAsync(Notify.EmailSentSuccessfully, "Email Sent Successfully..");

        return response;
    }
}




