

namespace AccountService.Business.DTOs;

public class CreateUserResult
{
    public bool Succeeded { get; set; }
    public List<string> Errors { get; set; }
}
