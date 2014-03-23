using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpUsingFileCache
{
  [Serializable]
  public class CustomObject
  {

    public Guid ID { get; set; }
    public string Name { get; set; }
    public DateTime DateCreated { get; set; }
    public decimal Amount { get; set; }

  }
}
