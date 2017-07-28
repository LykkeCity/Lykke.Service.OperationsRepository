using System;
using System.Collections.Generic;
using System.Text;
using Lykke.Service.OperationsRepository.Core.CashOperations;
using Microsoft.AspNetCore.Mvc;

namespace Lykke.Service.OperationsRepository.Controllers
{
    [Route("api/[controller]")]
    public class CashOutAttemptRepositoryController: Controller
    {
        private readonly ICashOutAttemptRepository _cashOutAttemptRepo;

        public CashOutAttemptRepositoryController(ICashOutAttemptRepository cashOutAttemptRepo)
        {
            _cashOutAttemptRepo = cashOutAttemptRepo;
        }
    }
}
