using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KazoeciaoOutputAnalyzer
{
    public abstract class StepDifference
    {
        // 修正行数
        public abstract int ModifiedStepNum { get; }
        // 追加行数
        public abstract int NewAddedStepNum { get; }
        // 削除行数
        public abstract int DeletedStepNum { get; }
        // 修正前の行数
        public abstract int OldTotalStepNum { get; }
        // 流用行数
        public abstract int DiversionStepNum { get; }

        /// <summary>
        /// 新規換算ステップ数
        /// </summary>
        /// <returns></returns>
        public int MeasuredStepNum()
        {
            return NewAddedStepNum + ModifiedStepNum + DeletedStepNum;
        }

        public override string ToString()
        {
            return string.Format("{0}(Modify), {1}(Add), {2}(Del), {3}(OldTotal), {4}(Div)",
                                ModifiedStepNum, NewAddedStepNum, DeletedStepNum, OldTotalStepNum, DiversionStepNum);
        }

    }
}
