using Abc.Data.Quantity;
using Abc.Domain.Quantity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abc.Infra.Quantity
{
    public class MeasuresRepository: UniqueEntityRepository<Measure, MeasureData>, IMeasuresRepository
    {
        public MeasuresRepository(QuantityDbContext c) : base(c, c.Measures)
        {
        }
        public override async Task<List<Measure>> Get()
        {
            var list = await createPaged(createFiltered(createSorted())); //sorteerimise algoritm

            HasNextPage = list.HasNextPage;
            HasPreviousPage = list.HasPreviousPage;

            return list.Select(e => new Measure(e)).ToList();
        }

        private async Task<PaginatedList<MeasureData>> createPaged(IQueryable<MeasureData> dataSet)
        {            
            return await PaginatedList<MeasureData>.CreateAsync(
                dataSet, PageIndex, PageSize);
        }

        private IQueryable<MeasureData> createFiltered(IQueryable<MeasureData> set)
        {
            if (String.IsNullOrEmpty(SearchString)) return set;
            {
                return set.Where(s => s.Name.Contains(SearchString)
                                       || s.Code.Contains(SearchString) 
                                       || s.Id.Contains(SearchString) 
                                       || s.Definition.Contains(SearchString) 
                                       || s.ValidFrom.ToString().Contains(SearchString) 
                                       || s.ValidTo.ToString().Contains(SearchString));
            }
        }

        private IQueryable<MeasureData> createSorted() //paritav, saab paringuid genereerida, db sisaldab MeasureData classi
        {
            IQueryable<MeasureData> measures = from s in dbSet select s;   //teeb paringu andmebaasi

            measures = setSorting(measures);
            return measures.AsNoTracking();
        }
    }
}
