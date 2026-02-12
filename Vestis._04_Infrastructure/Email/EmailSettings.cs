using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestis._04_Infrastructure.Email;

public sealed class EmailSettings
{
    public string ConnectionString { get; set; } = default!;
    public string SenderAddress { get; set; } = default!;
}
