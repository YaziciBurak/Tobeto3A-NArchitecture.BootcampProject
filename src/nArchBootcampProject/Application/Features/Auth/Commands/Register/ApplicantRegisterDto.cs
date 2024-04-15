using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.RefreshToken;
public class ApplicantRegisterDto : RegisterDto
{
    public string About { get; set; }
}
