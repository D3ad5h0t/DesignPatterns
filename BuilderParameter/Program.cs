var mailService = new EmailService();
mailService.SendEmail(builder =>
    builder.From("ava@uis.com")
        .To("babar@gmail.com")
        .Subject("Question")
        .Body("Hello World!"));