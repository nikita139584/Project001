
using System.ComponentModel.DataAnnotations;

namespace Project001.Models;

public class RegistrationViewModel
{
    [Required(ErrorMessage = "Введите имя")]
    [StringLength(50)]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Введите фамилию")]
    [StringLength(50)]
    public string LastName { get; set; } = "";

    [DataType(DataType.Date)]
    public DateTime? BirthDate { get; set; }

    public string? Gender { get; set; }

    [Required(ErrorMessage = "Введите email")]
    [EmailAddress(ErrorMessage = "Некорректный email")]
    [StringLength(120)]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Придумайте пароль")]
    [StringLength(100, MinimumLength = 6,
        ErrorMessage = "Пароль: от 6 до 100 символов")]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; } = "";

    [Required(ErrorMessage = "Повторите пароль")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
    [Display(Name = "Повторите пароль")]
    public string ConfirmPassword { get; set; } = "";

    [Required(ErrorMessage = "Введите телефон")]
    [StringLength(30)]
    public string Phone { get; set; } = "";

    [Required(ErrorMessage = "Выберите страну")]
    public string Country { get; set; } = "";

    [StringLength(100)]
    public string? City { get; set; }

    public bool Newsletter { get; set; }

    [Range(typeof(bool), "true", "true",
        ErrorMessage = "Необходимо принять соглашение")]
    public bool Agreement { get; set; }
}