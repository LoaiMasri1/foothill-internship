using AirportTicket.Features.Users.Models;
using AirportTicket.Features.Users.Models.Enums;
using System.Net.Mail;

namespace AirportTikcet.Test.Customization;

class UserCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        var user = fixture.Build<User>()
                              .With(u => u.Password,
                              "LoaiMasri123")
                              .With(u => u.Email,
                              fixture.Create<MailAddress>().Address)
                              .With(u => u.Role, UserRole.Passenger)
                              .Create();

        fixture.Inject(user);
    }
}
