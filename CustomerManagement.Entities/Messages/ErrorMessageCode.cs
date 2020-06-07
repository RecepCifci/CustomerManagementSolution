using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Entities.Messages
{
    public enum ErrorMessageCode
    {
        CustomerCouldNotFind = 1,
        CustomerInActive = 2,
        EmailAlreadyExists = 3,
        UserCouldNotInserted = 4,
        UserAlreadyActive = 5,
        ActivateIdDoesNotExists = 6,
        ProfileCouldNotUpdated = 7,
    }
}
