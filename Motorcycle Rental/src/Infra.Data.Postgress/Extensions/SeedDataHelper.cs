using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Infra.Data.Postgress.Extensions
{
    public static class SeedDataHelper
    {
        public static ModelBuilder SeedData(this ModelBuilder builder)
        {


            #region Curva

            //var _curva = new List<Caracteristica_Curva>()
            //{
            //    new Caracteristica_Curva
            //    {
            //        Id = new Guid("80E6BA0C-9221-40C0-B026-386D3E91067E"),
            //        CreatedDate = DateTime.Now,
            //        Descricao = "A"
            //    },
            //    new Caracteristica_Curva
            //    {
            //        Id = new Guid("E3CFABD5-7FD1-4987-92E8-866CBA212842"),
            //        CreatedDate = DateTime.Now,
            //        Descricao = "B"
            //    },
            //    new Caracteristica_Curva
            //    {
            //        Id = new Guid("E12FF318-7B66-41A4-8C1D-ABF097F40DD7"),
            //        CreatedDate = DateTime.Now,
            //        Descricao = "C"
            //    },
            //    new Caracteristica_Curva
            //    {
            //        Id = new Guid("D55E0870-D757-487A-83DA-CE4B6602CFFB"),
            //        CreatedDate = DateTime.Now,
            //        Descricao = "D"
            //    },
            //    new Caracteristica_Curva
            //    {
            //        Id = new Guid("04554869-A1FB-409E-8131-B83847505B2C"),
            //        CreatedDate = DateTime.Now,
            //        Descricao = "E"
            //    },
            //};

            //builder.Entity<Caracteristica_Curva>()
            //       .HasData(_curva);

            #endregion Curva

            return builder;
        }
    }
}