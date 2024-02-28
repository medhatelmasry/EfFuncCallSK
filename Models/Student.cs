using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfFuncCallSK.Models;
public class Student {
   public int StudentId { get; set; }
   public string? FirstName { get; set; }
   public string? LastName { get; set; }
   public string? School { get; set; }
 
   public override string ToString() {
      return $"Student ID: {StudentId}, First Name: {FirstName}, Last Name: {LastName}, School: {School}";
   }
}
