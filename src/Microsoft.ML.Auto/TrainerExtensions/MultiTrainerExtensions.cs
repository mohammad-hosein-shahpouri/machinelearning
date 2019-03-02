﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using Microsoft.ML.Trainers;

namespace Microsoft.ML.Auto
{
    using ITrainerEstimator = ITrainerEstimator<ISingleFeaturePredictionTransformer<object>, object>;
    using ITrainerEstimatorProducingFloat = ITrainerEstimator<ISingleFeaturePredictionTransformer<object>, object>;

    internal class AveragedPerceptronOvaExtension : ITrainerExtension
    {
        private static readonly ITrainerExtension _binaryLearnerCatalogItem = new AveragedPerceptronBinaryExtension();

        public IEnumerable<SweepableParam> GetHyperparamSweepRanges()
        {
            return SweepableParams.BuildAveragePerceptronParams();
        }

        public ITrainerEstimator CreateInstance(MLContext mlContext, IEnumerable<SweepableParam> sweepParams,
            ColumnInformation columnInfo)
        {
            var binaryTrainer = _binaryLearnerCatalogItem.CreateInstance(mlContext, sweepParams, columnInfo) as ITrainerEstimatorProducingFloat;
            return mlContext.MulticlassClassification.Trainers.OneVersusAll(binaryTrainer, labelColumnName: columnInfo.LabelColumn);
        }

        public PipelineNode CreatePipelineNode(IEnumerable<SweepableParam> sweepParams, ColumnInformation columnInfo)
        {
            return TrainerExtensionUtil.BuildOvaPipelineNode(this, _binaryLearnerCatalogItem, sweepParams, columnInfo);
        }
    }

    internal class FastForestOvaExtension : ITrainerExtension
    {
        private static readonly ITrainerExtension _binaryLearnerCatalogItem = new FastForestBinaryExtension();

        public IEnumerable<SweepableParam> GetHyperparamSweepRanges()
        {
            return SweepableParams.BuildFastForestParams();
        }

        public ITrainerEstimator CreateInstance(MLContext mlContext, IEnumerable<SweepableParam> sweepParams,
            ColumnInformation columnInfo)
        {
            var binaryTrainer = _binaryLearnerCatalogItem.CreateInstance(mlContext, sweepParams, columnInfo) as ITrainerEstimatorProducingFloat;
            return mlContext.MulticlassClassification.Trainers.OneVersusAll(binaryTrainer, labelColumnName: columnInfo.LabelColumn);
        }

        public PipelineNode CreatePipelineNode(IEnumerable<SweepableParam> sweepParams, ColumnInformation columnInfo)
        {
            return TrainerExtensionUtil.BuildOvaPipelineNode(this, _binaryLearnerCatalogItem, sweepParams, columnInfo);
        }
    }

    internal class LightGbmMultiExtension : ITrainerExtension
    {
        public IEnumerable<SweepableParam> GetHyperparamSweepRanges()
        {
            return SweepableParams.BuildLightGbmParams();
        }

        public ITrainerEstimator CreateInstance(MLContext mlContext, IEnumerable<SweepableParam> sweepParams,
            ColumnInformation columnInfo)
        {
            var options = TrainerExtensionUtil.CreateLightGbmOptions(sweepParams, columnInfo);
            return mlContext.MulticlassClassification.Trainers.LightGbm(options);
        }

        public PipelineNode CreatePipelineNode(IEnumerable<SweepableParam> sweepParams, ColumnInformation columnInfo)
        {
            return TrainerExtensionUtil.BuildLightGbmPipelineNode(TrainerExtensionCatalog.GetTrainerName(this), sweepParams,
                columnInfo.LabelColumn, columnInfo.WeightColumn);
        }
    }

    internal class LinearSvmOvaExtension : ITrainerExtension
    {
        private static readonly ITrainerExtension _binaryLearnerCatalogItem = new LinearSvmBinaryExtension();

        public IEnumerable<SweepableParam> GetHyperparamSweepRanges()
        {
            return SweepableParams.BuildLinearSvmParams();
        }

        public ITrainerEstimator CreateInstance(MLContext mlContext, IEnumerable<SweepableParam> sweepParams,
            ColumnInformation columnInfo)
        {
            var binaryTrainer = _binaryLearnerCatalogItem.CreateInstance(mlContext, sweepParams, columnInfo) as ITrainerEstimatorProducingFloat;
            return mlContext.MulticlassClassification.Trainers.OneVersusAll(binaryTrainer, labelColumnName: columnInfo.LabelColumn);
        }

        public PipelineNode CreatePipelineNode(IEnumerable<SweepableParam> sweepParams, ColumnInformation columnInfo)
        {
            return TrainerExtensionUtil.BuildOvaPipelineNode(this, _binaryLearnerCatalogItem, sweepParams, columnInfo);
        }
    }

    internal class SdcaMultiExtension : ITrainerExtension
    {
        public IEnumerable<SweepableParam> GetHyperparamSweepRanges()
        {
            return SweepableParams.BuildSdcaParams();
        }

        public ITrainerEstimator CreateInstance(MLContext mlContext, IEnumerable<SweepableParam> sweepParams,
            ColumnInformation columnInfo)
        {
            var options = TrainerExtensionUtil.CreateOptions<SdcaMultiClassTrainer.Options>(sweepParams, columnInfo.LabelColumn);
            return mlContext.MulticlassClassification.Trainers.StochasticDualCoordinateAscent(options);
        }

        public PipelineNode CreatePipelineNode(IEnumerable<SweepableParam> sweepParams, ColumnInformation columnInfo)
        {
            return TrainerExtensionUtil.BuildPipelineNode(TrainerExtensionCatalog.GetTrainerName(this), sweepParams,
                columnInfo.LabelColumn);
        }
    }

    internal class LogisticRegressionOvaExtension : ITrainerExtension
    {
        private static readonly ITrainerExtension _binaryLearnerCatalogItem = new LogisticRegressionBinaryExtension();

        public IEnumerable<SweepableParam> GetHyperparamSweepRanges()
        {
            return SweepableParams.BuildLogisticRegressionParams();
        }

        public ITrainerEstimator CreateInstance(MLContext mlContext, IEnumerable<SweepableParam> sweepParams,
            ColumnInformation columnInfo)
        {
            var binaryTrainer = _binaryLearnerCatalogItem.CreateInstance(mlContext, sweepParams, columnInfo) as ITrainerEstimatorProducingFloat;
            return mlContext.MulticlassClassification.Trainers.OneVersusAll(binaryTrainer, labelColumnName: columnInfo.LabelColumn);
        }

        public PipelineNode CreatePipelineNode(IEnumerable<SweepableParam> sweepParams, ColumnInformation columnInfo)
        {
            return TrainerExtensionUtil.BuildOvaPipelineNode(this, _binaryLearnerCatalogItem, sweepParams, columnInfo);
        }
    }

    internal class SgdOvaExtension : ITrainerExtension
    {
        private static readonly ITrainerExtension _binaryLearnerCatalogItem = new SgdBinaryExtension();

        public IEnumerable<SweepableParam> GetHyperparamSweepRanges()
        {
            return SweepableParams.BuildSgdParams();
        }

        public ITrainerEstimator CreateInstance(MLContext mlContext, IEnumerable<SweepableParam> sweepParams,
            ColumnInformation columnInfo)
        {
            var binaryTrainer = _binaryLearnerCatalogItem.CreateInstance(mlContext, sweepParams, columnInfo) as ITrainerEstimatorProducingFloat;
            return mlContext.MulticlassClassification.Trainers.OneVersusAll(binaryTrainer, labelColumnName: columnInfo.LabelColumn);
        }

        public PipelineNode CreatePipelineNode(IEnumerable<SweepableParam> sweepParams, ColumnInformation columnInfo)
        {
            return TrainerExtensionUtil.BuildOvaPipelineNode(this, _binaryLearnerCatalogItem, sweepParams, columnInfo);
        }
    }

    internal class SymSgdOvaExtension : ITrainerExtension
    {
        private static readonly ITrainerExtension _binaryLearnerCatalogItem = new SymSgdBinaryExtension();

        public IEnumerable<SweepableParam> GetHyperparamSweepRanges()
        {
            return _binaryLearnerCatalogItem.GetHyperparamSweepRanges();
        }

        public ITrainerEstimator CreateInstance(MLContext mlContext, IEnumerable<SweepableParam> sweepParams,
            ColumnInformation columnInfo)
        {
            var binaryTrainer = _binaryLearnerCatalogItem.CreateInstance(mlContext, sweepParams, columnInfo) as ITrainerEstimatorProducingFloat;
            return mlContext.MulticlassClassification.Trainers.OneVersusAll(binaryTrainer, labelColumnName: columnInfo.LabelColumn);
        }

        public PipelineNode CreatePipelineNode(IEnumerable<SweepableParam> sweepParams, ColumnInformation columnInfo)
        {
            return TrainerExtensionUtil.BuildOvaPipelineNode(this, _binaryLearnerCatalogItem, sweepParams, columnInfo);
        }
    }

    internal class FastTreeOvaExtension : ITrainerExtension
    {
        private static readonly ITrainerExtension _binaryLearnerCatalogItem = new FastTreeBinaryExtension();

        public IEnumerable<SweepableParam> GetHyperparamSweepRanges()
        {
            return SweepableParams.BuildFastTreeParams();
        }

        public ITrainerEstimator CreateInstance(MLContext mlContext, IEnumerable<SweepableParam> sweepParams,
            ColumnInformation columnInfo)
        {
            var binaryTrainer = _binaryLearnerCatalogItem.CreateInstance(mlContext, sweepParams, columnInfo) as ITrainerEstimatorProducingFloat;
            return mlContext.MulticlassClassification.Trainers.OneVersusAll(binaryTrainer, labelColumnName: columnInfo.LabelColumn);
        }

        public PipelineNode CreatePipelineNode(IEnumerable<SweepableParam> sweepParams, ColumnInformation columnInfo)
        {
            return TrainerExtensionUtil.BuildOvaPipelineNode(this, _binaryLearnerCatalogItem, sweepParams, columnInfo);
        }
    }

    internal class LogisticRegressionMultiExtension : ITrainerExtension
    {
        public IEnumerable<SweepableParam> GetHyperparamSweepRanges()
        {
            return SweepableParams.BuildLogisticRegressionParams();
        }

        public ITrainerEstimator CreateInstance(MLContext mlContext, IEnumerable<SweepableParam> sweepParams,
            ColumnInformation columnInfo)
        {
            var options = TrainerExtensionUtil.CreateOptions<MulticlassLogisticRegression.Options>(sweepParams, columnInfo.LabelColumn);
            options.WeightColumn = columnInfo.WeightColumn;
            return mlContext.MulticlassClassification.Trainers.LogisticRegression(options);
        }

        public PipelineNode CreatePipelineNode(IEnumerable<SweepableParam> sweepParams, ColumnInformation columnInfo)
        {
            return TrainerExtensionUtil.BuildPipelineNode(TrainerExtensionCatalog.GetTrainerName(this), sweepParams,
                columnInfo.LabelColumn, columnInfo.WeightColumn);
        }
    }
}