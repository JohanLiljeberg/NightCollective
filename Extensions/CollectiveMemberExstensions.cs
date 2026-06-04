using Night.Models;
using Night.ViewModels;

namespace Night.Extensions
{
    public static class CollectiveMemberExtensions
    {
        public static CMBasicInfoVM ToBasicInfoVM(this CollectiveMember member)
        {
            return new CMBasicInfoVM
            {
                Name = member.Name,
                Image = member.Image
            };
        }

        public static CollectiveMember ToCollectiveMember(this CMBasicInfoVM vm)
        {
            return new CollectiveMember
            {
                Name = vm.Name,
                Image = vm.Image
            };
        }
    }
}
