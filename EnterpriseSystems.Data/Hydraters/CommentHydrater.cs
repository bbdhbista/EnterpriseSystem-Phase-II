using EnterpriseSystems.Data.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using EnterpriseSystems.Data.Model.Constants;

namespace EnterpriseSystems.Data.Hydraters
{
    public class CommentHydrater : IHydrater<CommentVO>
    {
        public IEnumerable<CommentVO> Hydrate(DataTable dataTable)
        {
            var Comments = new List<CommentVO>();

            if (dataTable != null)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    var Comment = HydrateEntity(dataRow);
                    Comments.Add(Comment);
                }
            }

            return Comments;
        }

        private CommentVO HydrateEntity(DataRow dataRow)
        {
            var Comment = new CommentVO
            {
                Identity = (int)dataRow[CommentColumnNames.Identity],
                EntityName = dataRow[CommentColumnNames.EntityName].ToString(),
                EntityIdentity = (int)dataRow[CommentColumnNames.EntityIdentity],
                SequenceNumber = (short)dataRow[CommentColumnNames.SequenceNumber],
                CommentType = dataRow[CommentColumnNames.CommentType].ToString(),
                CommentText = dataRow[CommentColumnNames.CommentText].ToString(),
                CreatedDate = (DateTime?)dataRow[CommentColumnNames.CreatedDate],
                CreatedUserId = dataRow[CommentColumnNames.CreatedUserId].ToString(),
                CreatedProgramCode = dataRow[CommentColumnNames.CreatedProgramCode].ToString(),
                LastUpdatedDate = (DateTime?)dataRow[CommentColumnNames.LastUpdatedDate],
                LastUpdatedUserId = dataRow[CommentColumnNames.LastUpdatedUserId].ToString(),
                LastUpdatedProgramCode = dataRow[CommentColumnNames.LastUpdatedProgramCode].ToString()
            };
            return Comment;
        }
    }
}
