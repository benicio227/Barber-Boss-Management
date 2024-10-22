using BarberBossManagement.Communication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberBossManagement.Application.UseCases.Users.Update;
public interface IUpdateUserUseCase
{
    Task Execute(RequestUpdateUserJson request);
}
