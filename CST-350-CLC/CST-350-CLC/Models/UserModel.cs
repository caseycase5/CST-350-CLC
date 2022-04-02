using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CST_350_CLC.Models {
    public class UserModel {
        [DisplayName("User ID")]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(50, MinimumLength = 4)]
        [DisplayName("Email Address")]
        public string Email { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        [DisplayName("State")]
        public string State { get; set; }

        [Required]
        [DisplayName("Age")]
        public int Age { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 1)]
        [DisplayName("Sex")]
        public string Sex { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 4)]
        [DisplayName("Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6)]
        [DisplayName("Password")]
        public string Password { get; set; }

        public UserModel(int id, string firstName, string lastName, string email, string state, int age, string sex, string username, string password) {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            State = state;
            Age = age;
            Sex = sex;
            Username = username;
            Password = password;
        }

        public UserModel() {
        }
    }
}

