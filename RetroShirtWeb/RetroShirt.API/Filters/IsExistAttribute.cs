using Microsoft.AspNetCore.Mvc;

namespace RetroShirt.API.Filters
{
    public class IsExistAttribute:TypeFilterAttribute
    {
        public IsExistAttribute():base(typeof(CheckExisting)) 
            // typefilterattribute'a gönderdik yazdıgımız filtreyi.
            //biz DI kullandıgımız icin , işlemi checkexisting yapsın bu sonucu şey yapsın attribute olarak demek.

        {

        }

    }
}
