
using PublicApi.DTO.v1.Identity;
using Resources;
using Resources.BLL.App.DTO;
using Resources.DropDown;
using Resources.Views.Crud;
using Resources.Views.Shared;

namespace PublicApi.DTO.v1
{
    public class LangResources
    {
        public Views Views { get; set; } = new Views();
        public BLLAppDTO BLLAppDTO { get; set; } = new BLLAppDTO();
        public DropDown DropDown { get; set; } = new DropDown();
        public Crud Crud { get; set; } = new Crud();
        public AppRoles AppRole { get; set; } = new AppRoles();
        public AppUsers AppUser { get; set; } = new AppUsers();
        public Account Account { get; set; } = new Account();
        public Common Common { get; set; } = new Common();
    }
    public class Account
    {
        public string Email { get; set; } = Resources.Areas.Identity.Pages.Account.Login.Email;
        public string Password { get; set; } = Resources.Areas.Identity.Pages.Account.Login.Password;
        public string LogIn { get; set; } = Resources.Areas.Identity.Pages.Account.Login.LogIn;
        public string ConfirmPassword { get; set; } = Resources.Areas.Identity.Pages.Account.Register.ConfirmPassword;
        public string FirstName { get; set; } = Resources.Areas.Identity.Pages.Account.Register.FirstName;
        public string LastName { get; set; } = Resources.Areas.Identity.Pages.Account.Register.LastName;
        public string Register { get; set; } = Resources.Areas.Identity.Pages.Account.Register.ButtonRegister;
        public string PasswordDontMatch { get; set; } = Resources.Areas.Identity.Pages.Account.Register.PasswordsDontMatch;


    }
    public class Common
    {
        public string MinLength { get; set; } = Resources.Common.MinLength;
        public string MaxLength { get; set; } = Resources.Common.ErrorMessage_MaxLength;
        public string Required { get; set; } = Resources.Common.Required;
        public string LoginProblem { get; set; } = Resources.Common.UserPasswordProblem;
        public string AlreadyRegistered { get; set; } = Resources.Common.IncorrectEmail;
        public string EmailNotFound { get; set; } = Resources.Common.EmailNotFound;


    }


    public class BLLAppDTO {
        public Bookings Bookings { get; set; } = new Bookings();
        public Categories Categories { get; set; } = new Categories();
        public Cities Cities { get; set; } = new Cities();
        public Conditions Conditions { get; set; } = new Conditions();
        public Counties Counties { get; set; } = new Counties();
        public Materials Materials { get; set; } = new Materials();
        public MessageForms MessageForms { get; set; } = new MessageForms();
        public Pictures Pictures { get; set; } = new Pictures();
        public ProductMaterials ProductMaterials { get; set; } = new ProductMaterials();
        public Products Products { get; set; } = new Products();
        public Units Units { get; set; } = new Units();
        public UserBookedProduct UserBookedProduct { get; set; } = new UserBookedProduct();
        public UserMessage UserMessage { get; set; } = new UserMessage();
    }


    public class AppRoles
    {
        public string AppRole { get; set; } = Resources.Views.AppRole.AppRoles.AppRole;
        public string Name { get; set; } = Resources.Views.AppRole.AppRoles.Name;
        public string AddTo { get; set; } = Resources.Views.AppRole.AppRoles.AddTo;
        public string ConcurrencyStamp { get; set; } = Resources.Views.AppRole.AppRoles.ConcurrencyStamp;
        public string NormalizedName { get; set; } = Resources.Views.AppRole.AppRoles.NormalizedName;
        public string RemoveFrom { get; set; } = Resources.Views.AppRole.AppRoles.RemoveFrom;

    }
    public class AppUsers
    {
        public string AppUser { get; set; } = Resources.Views.AppUser.AppUser.AppUsers;
        public string Email { get; set; } = Resources.Views.AppUser.AppUser.Email;
        public string Firstname { get; set; } = Resources.Views.AppUser.AppUser.Firstname;
        public string Lastname { get; set; } = Resources.Views.AppUser.AppUser.Lastname;
        public string Username { get; set; } = Resources.Views.AppUser.AppUser.Username;
        public string ConcurrencyStamp { get; set; } = Resources.Views.AppUser.AppUser.ConcurrencyStamp;
        public string EmailConfirmed { get; set; } = Resources.Views.AppUser.AppUser.EmailConfirmed;
        public string LockoutEnabled { get; set; } = Resources.Views.AppUser.AppUser.LockoutEnabled;
        public string LockoutEnd { get; set; } = Resources.Views.AppUser.AppUser.LockoutEnd;
        public string NormalizedEmail { get; set; } = Resources.Views.AppUser.AppUser.NormalizedEmail;
        public string PasswordHash { get; set; } = Resources.Views.AppUser.AppUser.PasswordHash;
        public string PhoneNumber { get; set; } = Resources.Views.AppUser.AppUser.PhoneNumber;
        public string SecurityStamp { get; set; } = Resources.Views.AppUser.AppUser.SecurityStamp;
        public string AccessFailedCount { get; set; } = Resources.Views.AppUser.AppUser.AccessFailedCount;
        public string NormalizedUserName { get; set; } = Resources.Views.AppUser.AppUser.NormalizedUserName;
        public string PhoneNumberConfirmed { get; set; } = Resources.Views.AppUser.AppUser.PhoneNumberConfirmed;
        public string TwoFactorEnabled { get; set; } = Resources.Views.AppUser.AppUser.TwoFactorEnabled;

    }

    public class Crud
    {
        public string Confirmation { get; set; } = Resources.Views.Crud.CRUD.Confirmation;
        public string Create { get; set; } = Resources.Views.Crud.CRUD.Create;
        public string Delete { get; set; } = Resources.Views.Crud.CRUD.Delete;
        public string Details { get; set; } = Resources.Views.Crud.CRUD.Details;
        public string Edit { get; set; } = Resources.Views.Crud.CRUD.Edit;
        public string Index { get; set; } = Resources.Views.Crud.CRUD.Index;
        public string Reply { get; set; } = Resources.Views.Crud.CRUD.Reply;
        public string View { get; set; } = Resources.Views.Crud.CRUD.View;
        public string ChangeUserRoles { get; set; } = Resources.Views.Crud.CRUD.ChangeUserRoles;
        public string DeleteConfirm { get; set; } = Resources.Views.Crud.CRUD.DeleteConfirm;


    }

    public class DropDown
    {
        public string Select { get; set; } = Resources.DropDown.DropDown.Select;
    }

    public class Bookings
    {
        public string Booking { get; set; } = Resources.BLL.App.DTO.Bookings.Booking;
        public string FindAProduct { get; set; } = Resources.BLL.App.DTO.Bookings.FindAProduct;
        public string Reserve { get; set; } = Resources.BLL.App.DTO.Bookings.Reserve;
        public string TimeBooked { get; set; } = Resources.BLL.App.DTO.Bookings.TimeBooked;
        public string Until { get; set; } = Resources.BLL.App.DTO.Bookings.Until;

    }
    public class Categories
    {
        public string Category { get; set; } = Resources.BLL.App.DTO.Categories.Category;
        public string Name { get; set; } = Resources.BLL.App.DTO.Categories.Name;

    }
    public class Cities
    {
        public string City { get; set; } = Resources.BLL.App.DTO.Cities.City;
        public string Name { get; set; } = Resources.BLL.App.DTO.Cities.Name;

    }
    public class Conditions
    {
        public string Condition { get; set; } = Resources.BLL.App.DTO.Conditions.Condition;
        public string Description { get; set; } = Resources.BLL.App.DTO.Conditions.Description;

    }
    public class Counties
    {
        public string County { get; set; } = Resources.BLL.App.DTO.Counties.County;
        public string Name { get; set; } = Resources.BLL.App.DTO.Counties.Name;

    }
    public class Materials
    {
        public string Material { get; set; } = Resources.BLL.App.DTO.Materials.Material;
        public string Name { get; set; } = Resources.BLL.App.DTO.Materials.Name;
        public string Comment { get; set; } = Resources.BLL.App.DTO.Materials.Comment;

    }
    public class MessageForms
    {
        public string Email { get; set; } = Resources.BLL.App.DTO.MessageForms.Email;
        public string Message { get; set; } = Resources.BLL.App.DTO.MessageForms.Message;
        public string Subject { get; set; } = Resources.BLL.App.DTO.MessageForms.Subject;
        public string DateSent { get; set; } = Resources.BLL.App.DTO.MessageForms.DateSent;
        public string MessageForm { get; set; } = Resources.BLL.App.DTO.MessageForms.MessageForm;
        public string SendMessage { get; set; } = Resources.BLL.App.DTO.MessageForms.SendMessage;

    }
    public class Pictures
    {
        public string Picture { get; set; } = Resources.BLL.App.DTO.Pictures.Picture;
        public string Url { get; set; } = Resources.BLL.App.DTO.Pictures.Url;
        public string ProductName { get; set; } = Resources.BLL.App.DTO.Pictures.ProductName;

    }
    public class ProductMaterials
    {
        public string ProductMaterial { get; set; } = Resources.BLL.App.DTO.ProductMaterials.ProductMaterial;

    }
    public class Products
    {
        public string Product { get; set; } = Resources.BLL.App.DTO.Products.Product;
        public string Category { get; set; } = Resources.BLL.App.DTO.Products.Category;
        public string City { get; set; } = Resources.BLL.App.DTO.Products.City;
        public string County { get; set; } = Resources.BLL.App.DTO.Products.County;
        public string Color { get; set; } = Resources.BLL.App.DTO.Products.Color;
        public string Description { get; set; } = Resources.BLL.App.DTO.Products.Description;
        public string Depth { get; set; } = Resources.BLL.App.DTO.Products.Depth;
        public string DateAdded { get; set; } = Resources.BLL.App.DTO.Products.DateAdded;
        public string Height { get; set; } = Resources.BLL.App.DTO.Products.Height;
        public string Width { get; set; } = Resources.BLL.App.DTO.Products.Width;
        public string LocationDescription { get; set; } = Resources.BLL.App.DTO.Products.LocationDescription;
        public string Location { get; set; } = Resources.BLL.App.DTO.Products.Location;
        public string HasTransport { get; set; } = Resources.BLL.App.DTO.Products.HasTransport;
        public string IsBooked { get; set; } = Resources.BLL.App.DTO.Products.IsBooked;
        public string MyProduct { get; set; } = Resources.BLL.App.DTO.Products.MyProduct;
        public string Material { get; set; } = Resources.BLL.App.DTO.Products.Material;
        public string Unit { get; set; } = Resources.BLL.App.DTO.Products.Unit;
        public string RecentlyAddedProducts { get; set; } = Resources.BLL.App.DTO.Products.RecentlyAddedProducts;
        public string Condition { get; set; } = Resources.BLL.App.DTO.Products.Condition;
        public string Picture { get; set; } = Resources.BLL.App.DTO.Products.Picture;
        public string ProductAge { get; set; } = Resources.BLL.App.DTO.Products.ProductAge;
        public string Size { get; set; } = Resources.BLL.App.DTO.Products.Size;
    }
    public class Units
    {
        public string Name { get; set; } = Resources.BLL.App.DTO.Units.Name;
        public string Unit { get; set; } = Resources.BLL.App.DTO.Units.Unit;


    }
    public class UserBookedProduct
    {
        public string ProductOwner { get; set; } = Resources.BLL.App.DTO.UserBookedProduct.ProductOwner;
        public string UserBookedProducts { get; set; } = Resources.BLL.App.DTO.UserBookedProduct.UserBookedProducts;


    }
    public class UserMessage
    {
        public string Email { get; set; } = Resources.BLL.App.DTO.UserMessage.Email;
        public string From { get; set; } = Resources.BLL.App.DTO.UserMessage.From;
        public string Message { get; set; } = Resources.BLL.App.DTO.UserMessage.Message;
        public string Subject { get; set; } = Resources.BLL.App.DTO.UserMessage.Subject;
        public string DateSent { get; set; } = Resources.BLL.App.DTO.UserMessage.DateSent;
        public string NewMessage { get; set; } = Resources.BLL.App.DTO.UserMessage.NewMessage;
        public string SenderEmail { get; set; } = Resources.BLL.App.DTO.UserMessage.SenderEmail;
        public string UserMessages { get; set; } = Resources.BLL.App.DTO.UserMessage.UserMessages;


    }

    public class Views
    {
        public Shared Shared { get; set; } = new Shared();


    }

    public class Shared
    {
        public Layout Layout { get; set; } = new Layout();
        public Buttons Buttons { get; set; } = new Buttons();
        public LoginPartial LoginPartial { get; set; } = new LoginPartial();

    }
    public class LoginPartial
    {
        public string Hello { get; set; } = Resources.Views.Shared._LoginPartial.Hello;
        public string Login { get; set; } = Resources.Views.Shared._LoginPartial.Login;
        public string Logout { get; set; } = Resources.Views.Shared._LoginPartial.Logout;
        public string Register { get; set; } = Resources.Views.Shared._LoginPartial.Register;

    }

    public class Buttons
    {
        public string Create { get; set; } = Resources.Views.Shared.Buttons.Create;
        public string Delete { get; set; } = Resources.Views.Shared.Buttons.Delete;
        public string Remove { get; set; } = Resources.Views.Shared.Buttons.Remove;
        public string Save { get; set; } = Resources.Views.Shared.Buttons.Save;
        public string Search { get; set; } = Resources.Views.Shared.Buttons.Search;
        public string Send { get; set; } = Resources.Views.Shared.Buttons.Send;
    }

    public class Layout
    {
        public string Languages { get; set; } = Resources.Views.Shared._Layout.Languages;
        public string Roles { get; set; } = Resources.Views.Shared._Layout.Roles;
        public string Search { get; set; } = Resources.Views.Shared._Layout.Search;
        public string UseLocalAccount { get; set; } = Resources.Views.Shared._Layout.UseLocalAccount;
        public string UserMessages { get; set; } = Resources.Views.Shared._Layout.UserMessages;
        public string Users { get; set; } = Resources.Views.Shared._Layout.Users;

    }

}