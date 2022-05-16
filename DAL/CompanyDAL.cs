using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

using desa.Interfaces;
using desa.Models;

namespace desa.DAL {
public class CompanyDAL:ICompany
{
   private static readonly HttpClient client;
   static CompanyDAL()
   {
      client = new HttpClient();
   }

   public async Task<List<CompanyModel>> GetCompanies() {
    
     var clonedList = new List<CompanyModel>();
     var remainedList = new List<CompanyModel>();
     var result = new List<CompanyModel>();
     var distinct_result = new List<CompanyModel>();
     var list_item = new List<CompanyModel>();
     char charSeparators = '|';

     var response = await client.GetAsync("https://webservice.trueomni.com/json.aspx?domainid=2248&fn=listings");

      if (response.IsSuccessStatusCode)
      {
        var stringResponse = await response.Content.ReadAsStringAsync();
        result = JsonSerializer.Deserialize<List<CompanyModel>>(stringResponse);

         distinct_result = result.GroupBy(
                i => i.ListingID + 
                i.Company + 
                i.Image_List + 
                i.CategoryID).
              Select(i => i.FirstOrDefault()).ToList();

         clonedList = distinct_result.ToList();         
         remainedList = distinct_result.Take(9).ToList();

         distinct_result.AddRange(clonedList);
         distinct_result.AddRange(remainedList);

         foreach (var item in distinct_result)
         {
             string imagenes = item.Image_List;
             if (String.IsNullOrEmpty(imagenes) == false ) {
               int lugar = imagenes.IndexOf("|");
               if (lugar > 0 ) {
                 string stringBeforeChar = imagenes.Substring(0, imagenes.IndexOf("|"));
                 item.Image_List = stringBeforeChar;
               } else {
                 item.Image_List = imagenes;
               }                               
             } else {
               item.Image_List = "No image to display";
             }
                          
            
         }

      }
       else
       {
         throw new HttpRequestException(response.ReasonPhrase);
       }
 
    return distinct_result;

   }

}
}