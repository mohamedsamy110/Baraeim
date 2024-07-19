namespace Graduation_Project.Dtos
{
    public class ForgotPasswordRequestDto
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string OTP { get; set; }
    }
}
