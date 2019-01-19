using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KazoeciaoOutputAnalyzer
{
    public interface IStepDifference
    {
        /// <summary>
        /// 修正行数
        /// </summary>
        int ModifiedStepNum { get; }
        /// <summary>
        /// 追加行数
        /// </summary>
        int NewAddedStepNum { get; }
        /// <summary>
        /// 削除行数
        /// </summary>
        int DeletedStepNum { get; }
        // 流用行数
        int DiversionStepNum { get; }
    }
}
