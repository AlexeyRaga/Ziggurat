using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ziggurat.Contracts.Definition;
using Ziggurat.Infrastructure.DocumentStore;

namespace Ziggurat.Definition.Client
{
    public sealed class FormsProjectLayout
    {
        public Dictionary<string, List<Guid>> BlockHeaderForms  { get; set; }

        public FormsProjectLayout()
        {
            BlockHeaderForms = new Dictionary<string, List<Guid>>();
        }
    }

    public sealed class FormsProjectLayoutProjection
    {
        private readonly IDocumentWriter<Guid, FormsProjectLayout> _writer;
        public FormsProjectLayoutProjection(IDocumentStore store)
        {
            _writer = store.GetWriter<Guid, FormsProjectLayout>();
        }

        public void When(FormMovedInProjectLayout evt)
        {
            _writer.AddOrUpdate(evt.ProjectLayoutId, layout =>
            {
                List<Guid> formsInBlockHeader;
                if (!layout.BlockHeaderForms.TryGetValue(evt.BlockHeader, out formsInBlockHeader))
                {
                    formsInBlockHeader = new List<Guid>();
                    layout.BlockHeaderForms.Add(evt.BlockHeader, formsInBlockHeader);
                }

                var currentGroup = layout.BlockHeaderForms.FirstOrDefault(x => x.Value.Contains(evt.FormId));
                if (currentGroup.Value != null) currentGroup.Value.Remove(evt.FormId);

                if (evt.Order >= formsInBlockHeader.Count)
                    formsInBlockHeader.Add(evt.FormId);
                else
                    formsInBlockHeader.Insert(evt.Order, evt.FormId);
            });
        }
    }
}
