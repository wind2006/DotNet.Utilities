using YanZhiwei.DotNet2.Utilities.Common;

namespace YanZhiwei.DotNet2.UtilitiesExamples
{
    public class ProcessHelperExample
    {
        public static void ExecBatCommand()
        {
            ProcessHelper.ExecBatCommand(cmd =>
            {
                cmd("ipconfig");
                cmd("getmac");
                cmd("exit 0");
            });
        }
    }
}