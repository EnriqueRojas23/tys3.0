using System;

namespace CargaClic.ReadRepository.Contracts.Seguimiento.Results
{
    public class GetAllValorTablaResult
    {
            public long idvalortabla	{get;set;}
            public string valor	{get;set;}
            public int idmaestrotabla {get;set;}
            public bool activo {get;set;}
            public int  orden	{get;set;}
    }
}