using PMS.Shared.Contracts;

namespace PMS.Tests.Fakes
{
    internal class FakeCurrentUser : ICurrentUser
    {
        public FakeCurrentUser()
        {
            this.UserId = "4c4fc7dc-a8d6-4545-94f8-da3d3cdc2b47";
        }

        public string UserId { get; set; }
    }
}