using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RentAGym.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RentAGym.Persistence
{
    public static class DbInitializer
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;
            using var context = services.GetRequiredService<ApplicationDbContext>();
            using var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            #region Users

            var landLord = new Landlord
            {
                Email = "user1@gmail.com",
                EmailConfirmed = true,
                UserName = "user1@gmail.com"
            };
            await userManager.CreateAsync(landLord, "123456");
            var user1 = await userManager.FindByEmailAsync(landLord.Email);
            await userManager.AddClaimsAsync(user1, new Claim[] {
                //new Claim(ClaimTypes.Name, user1.UserName),
                new Claim(ClaimTypes.Email, user1.Email),
                //new Claim(ClaimTypes.NameIdentifier, user1.Id),
                new Claim("landLord","true", ClaimValueTypes.Boolean)
            });

            var tenant = new Tenant
            {
                Email = "user2@gmail.com",
                EmailConfirmed = true,
                UserName = "user2@gmail.com"
            };
            await userManager.CreateAsync(tenant, "123456");
            var user2 = await userManager.FindByEmailAsync(tenant.Email);
            await userManager.AddClaimsAsync(user2, new Claim[] {
                //new Claim(ClaimTypes.Name, user2.UserName),
                new Claim(ClaimTypes.Email, user2.Email),
                //new Claim(ClaimTypes.NameIdentifier, user2.Id),
                new Claim("tenant","true", ClaimValueTypes.Boolean)
            });



            #endregion

            #region Regions

            await context.Regions.AddRangeAsync(new Region[]
            {
                new Region{ Name="Минская"},
                new Region{ Name="Брестская"},
                new Region{ Name="Витебская"},
                new Region{ Name="Могилевская"},
                new Region{ Name="Гродненская"},
                new Region{ Name="Гомельская"},
            });
            await context.SaveChangesAsync();
            #endregion

            #region Facility

            var minsk = await context.Regions.FirstOrDefaultAsync(r => r.Name.Equals("Минская"));
            var vitebsk = await context.Regions.FirstOrDefaultAsync(r => r.Name.Equals("Витебская"));


            Facility[] facilities = new Facility[]
            {
                new Facility{ Country="Беларусь", Region=minsk, City="Минск",
                    Name="Олимпийский Спортивно-оздоровительный Комплекс",
                    Address="Сурганова 2а/3",
                    Longitude= 53.91749375577098, Latitude= 27.604650603016054
                },
                new Facility{Country="Беларусь", Region=vitebsk, City="Витебск",
                    Name="Фитнес клуб Витебск Xline на Московском",
                    Address="Московский пр. 70",
                    Longitude = 55.17828843492645, Latitude = 30.233472703326985
                }
            };

            landLord = await context.LandLords.FindAsync(user1.Id);
            landLord.Facilities.AddRange(facilities);
            await context.SaveChangesAsync();
            #endregion

            #region HallTypes
            HallType[] hallTypes = new HallType[]
            {
                new HallType{Name="Фитнес"},
                new HallType{Name="Танцы"},
                new HallType{Name="Борьба"},
                new HallType{Name="Велотрек"}
            };
            await context.HallTypes.AddRangeAsync(hallTypes);
            await context.SaveChangesAsync();
            #endregion

            #region Options

            var options = new Option[]
            {
                new Option{ Name="душевая"},
                new Option{ Name="раздевалки"},
                new Option{Name="зеркала"}
            };
            await context.Options.AddRangeAsync(options);
            await context.SaveChangesAsync();

            #endregion

            #region WorkSchedule
            WorkSchedulePiece[] works = new WorkSchedulePiece[]
            {
                new WorkSchedulePiece(){WorkFrom = TimeOnly.Parse("9:00"),WorkTo=TimeOnly.Parse("23:00")},
                new WorkSchedulePiece(){WorkFrom = TimeOnly.Parse("9:00"),WorkTo=TimeOnly.Parse("23:00")},
                new WorkSchedulePiece(){WorkFrom = TimeOnly.Parse("9:00"),WorkTo=TimeOnly.Parse("23:00")},
                new WorkSchedulePiece(){WorkFrom = TimeOnly.Parse("9:00"),WorkTo=TimeOnly.Parse("23:00")},
                new WorkSchedulePiece(){WorkFrom = TimeOnly.Parse("9:00"),WorkTo=TimeOnly.Parse("23:00")},
                new WorkSchedulePiece(){WorkFrom = TimeOnly.Parse("10:00"),WorkTo=TimeOnly.Parse("21:00")},
                new WorkSchedulePiece(){WorkFrom = TimeOnly.Parse("10:00"),WorkTo=TimeOnly.Parse("21:00")}
            };
            #endregion


            #region Halls

            Hall[] halls = new Hall[]
            {
                new Hall{LandlordId=landLord.Id, BasePrice=50, Area= 100, Name="Зал для волейбола/баскетбола",Payment=false,
                    Description="xxxx xxxxx xxxx xxxx", Options=options.ToList(),
                    HallType=hallTypes[0],WorkSchedule=works.Select(w => new WorkSchedulePiece{WorkFrom=w.WorkFrom, WorkTo = w.WorkTo }).ToList(), Images =new List<ImageData>{new ImageData {
                        ImageUri = "images/1.jpg", Name="1"} }
                },
                new Hall{LandlordId=landLord.Id, BasePrice=150, Area= 200, Name="Зал борьбы",Payment=false,
                    Description="xxxx xxxxx xxxx xxxx", Options=options.Take(2).ToList(),
                    HallType=hallTypes[2],WorkSchedule=works.Select(w => new WorkSchedulePiece{WorkFrom=w.WorkFrom, WorkTo = w.WorkTo }).ToList(), Images=new List<ImageData>{new ImageData {
                        ImageUri = "images/2.jpg", Name="2"} }
                }
            };
            landLord.Facilities[0].Halls.AddRange(halls);
            halls = new Hall[]
            {
                new Hall{LandlordId=landLord.Id, BasePrice=50, Area= 100, Name="Фитнес зал",Payment=false,
                    Description="xxxx xxxxx xxxx xxxx",WorkSchedule=works.Select(w => new WorkSchedulePiece{WorkFrom=w.WorkFrom, WorkTo = w.WorkTo }).ToList(), Options=options.ToList(),
                    HallType=hallTypes[1], Images=new List<ImageData>{new ImageData {
                        ImageUri = "images/3.jpg", Name="фитнес"} }
                },
                new Hall{LandlordId=landLord.Id, BasePrice=150, Area= 200, Name="Зал \"Растяжение\"",Payment=false,
                    Description="xxxx xxxxx xxxx xxxx",WorkSchedule=works.Select(w => new WorkSchedulePiece{WorkFrom=w.WorkFrom, WorkTo = w.WorkTo }).ToList(), Options=options.Take(2).ToList(),
                    HallType=hallTypes[1], Images=new List<ImageData>{new ImageData {
                        ImageUri = "images/4.jpg", Name="Растяжка"} }
                }
            };
            landLord.Facilities[1].Halls.AddRange(halls);

           

            await context.SaveChangesAsync();

            #endregion

            #region Reviews
            Review[] reviews = new Review[]
            {
                new Review{Mark=5,Name="Title1",Username="user1@gmail.com",Contents="..........",Hall=halls[0]},
                new Review{Mark=3,Name="Title2",Username="user2@gmail.com",Contents="wewewewewew",Hall=halls[1]}
            };
            await context.Reviews.AddRangeAsync(reviews);
            halls[0].ReviewCount=1;
            halls[1].ReviewCount=1;
            halls[0].OverallRating = 5;
            halls[1].OverallRating = 3;
            await context.SaveChangesAsync();
            
            #endregion

          /*  #region Schedule
            ReservedSchedule[] schedules = new ReservedSchedule[]
            {
                new ReservedSchedule(){HallId=halls[0].Id,TenantId=tenant.Id, ReservedHour = DateTime.ParseExact("16/11/2023 11:00", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)},
                new ReservedSchedule(){HallId=halls[0].Id,TenantId=tenant.Id, ReservedHour = DateTime.ParseExact("17/11/2023 11:00", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)},
                new ReservedSchedule(){HallId=halls[1].Id,TenantId=tenant.Id, ReservedHour = DateTime.ParseExact("17/11/2023 12:00", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)},
                new ReservedSchedule(){HallId=halls[1].Id,TenantId=tenant.Id, ReservedHour = DateTime.ParseExact("16/11/2023 11:00", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)}
            };

            await context.ReservedSchedules.AddRangeAsync(schedules);
            await context.SaveChangesAsync();

            ReservedSchedule[] fullDay = new ReservedSchedule[]
            {
                new ReservedSchedule(){HallId=halls[0].Id,TenantId=tenant.Id, ReservedHour = DateTime.ParseExact("18/11/2023 09:00", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)},
                new ReservedSchedule(){HallId=halls[0].Id,TenantId=tenant.Id, ReservedHour = DateTime.ParseExact("18/11/2023 10:00", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)},
                new ReservedSchedule(){HallId=halls[0].Id,TenantId=tenant.Id, ReservedHour = DateTime.ParseExact("18/11/2023 11:00", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)},
                new ReservedSchedule(){HallId=halls[0].Id,TenantId=tenant.Id, ReservedHour = DateTime.ParseExact("18/11/2023 12:00", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)},
                new ReservedSchedule(){HallId=halls[0].Id,TenantId=tenant.Id, ReservedHour = DateTime.ParseExact("18/11/2023 13:00", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)},
                new ReservedSchedule(){HallId=halls[0].Id,TenantId=tenant.Id, ReservedHour = DateTime.ParseExact("18/11/2023 14:00", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)},
                new ReservedSchedule(){HallId=halls[0].Id,TenantId=tenant.Id, ReservedHour = DateTime.ParseExact("18/11/2023 15:00", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)},
                new ReservedSchedule(){HallId=halls[0].Id,TenantId=tenant.Id, ReservedHour = DateTime.ParseExact("18/11/2023 16:00", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)},
                new ReservedSchedule(){HallId=halls[0].Id,TenantId=tenant.Id, ReservedHour = DateTime.ParseExact("18/11/2023 17:00", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)},
                new ReservedSchedule(){HallId=halls[0].Id,TenantId=tenant.Id, ReservedHour = DateTime.ParseExact("18/11/2023 18:00", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)},
                new ReservedSchedule(){HallId=halls[0].Id,TenantId=tenant.Id, ReservedHour = DateTime.ParseExact("18/11/2023 19:00", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)},
                new ReservedSchedule(){HallId=halls[0].Id,TenantId=tenant.Id, ReservedHour = DateTime.ParseExact("18/11/2023 20:00", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)},
                new ReservedSchedule(){HallId=halls[0].Id,TenantId=tenant.Id, ReservedHour = DateTime.ParseExact("18/11/2023 21:00", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)},
                new ReservedSchedule(){HallId=halls[0].Id,TenantId=tenant.Id, ReservedHour = DateTime.ParseExact("18/11/2023 22:00", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)},
                new ReservedSchedule(){HallId=halls[0].Id,TenantId=tenant.Id, ReservedHour = DateTime.ParseExact("18/11/2023 23:00", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)},
            };
            await context.ReservedSchedules.AddRangeAsync(fullDay);
            await context.SaveChangesAsync();

            #endregion
*/

        }
    }
}
