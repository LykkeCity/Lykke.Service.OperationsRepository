# Run this file if you use PowerShell directly
autorest -Input http://localhost:5000/swagger/v1/swagger.json -CodeGenerator CSharp -OutputDirectory ./AutorestClient -Namespace Lykke.Service.OpeationsRepository.AutorestClient