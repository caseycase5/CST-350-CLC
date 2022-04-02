using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CST_350_CLC.Models {
    public class UserModel {
        [DisplayName("User ID")]
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        [DisplayName("User First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        [DisplayName("User Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(20, MinimumLength = 4)]
        [DisplayName("User Email Address")]
        public string Email { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 4)]
        [DisplayName("User State")]
        public string State { get; set; }

        [Required]
        [DisplayName("User Age")]
        public int Age { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        [DisplayName("User Sex")]
        public string Sex { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 4)]
        [DisplayName("Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 4)]
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

