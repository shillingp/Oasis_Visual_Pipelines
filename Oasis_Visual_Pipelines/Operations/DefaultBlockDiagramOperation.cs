using CommunityToolkit.Mvvm.Input;
using Oasis_Visual_Pipelines.Attributes;
using Oasis_Visual_Pipelines.Classes;
using Oasis_Visual_Pipelines.Controls;
using Oasis_Visual_Pipelines.Dialogs;
using Oasis_Visual_Pipelines.Enums;
using Oasis_Visual_Pipelines.Functions;
using Oasis_Visual_Pipelines.Models;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Input;

namespace Oasis_Visual_Pipelines.Operations
{
    [BlockOperationGroup(BlockOperationType.None, BlockOperationGrouping.Other)]
    public class DefaultBlockDiagramOperation : BaseBlockDiagramOperation
    {
        public override int MaxInputs => 0;
        public override int MaxOutputs => 0;
        public override string OperationTitle => "Select Block";

        public override BlockOperationResult ExecuteOperation(params BlockOperationResult[] inputOperations)
        {
            return new BlockOperationResult(additionalOperations => null);
        }

        public static ICommand ChooseBlockTypeCommand => new RelayCommand<BlockControl>(async (control) =>
        {
            Type blockOperationInterface = typeof(BaseBlockDiagramOperation);

            BlockControl[] blockOperationInstances = GenerateBlockControlInstancesForClassesDerivedFromTypeFaster(blockOperationInterface);

            Block? chosenBlock = (Block?)await DialogHostFunctions.CreateAndShowDialog(
                new BlockPickerDialog(),
                blockOperationInstances,
                closeOnClickAway: true);
            if (chosenBlock is null) return;

            System.Windows.Point newPosition = control!.Block.Position;
            newPosition.X += chosenBlock.CanvasElement.ActualWidth / 2;
            newPosition.Y += chosenBlock.CanvasElement.ActualHeight / 2;

            control.Block.BlockDiagram!.AddBlock(
                newPosition,
                null,
                ((dynamic)chosenBlock).Data);

            control.Block.BlockDiagram.BlockDiagramItems.Remove(control.Block);
        });

        private delegate T ObjectActivator<T>(params object[] args);
        private static ObjectActivator<T> GetActivator<T>(ConstructorInfo ctor)
        {
            ParameterInfo[] paramsInfo = ctor.GetParameters();

            //create a single param of type object[]
            ParameterExpression param =
                Expression.Parameter(typeof(object[]), "args");

            Expression[] argsExp =
                new Expression[paramsInfo.Length];

            //pick each arg from the params array 
            //and create a typed expression of them
            for (int i = 0; i < paramsInfo.Length; i++)
            {
                Expression index = Expression.Constant(i);
                Type paramType = paramsInfo[i].ParameterType;

                Expression paramAccessorExp =
                    Expression.ArrayIndex(param, index);

                Expression paramCastExp =
                    Expression.Convert(paramAccessorExp, paramType);

                argsExp[i] = paramCastExp;
            }

            //make a NewExpression that calls the
            //ctor with the args we just created
            NewExpression newExp = Expression.New(ctor, argsExp);

            //create a lambda with the New
            //Expression as body and our param object[] as arg
            LambdaExpression lambda =
                Expression.Lambda(typeof(ObjectActivator<T>), newExp, param);

            //compile it
            ObjectActivator<T> compiled = (ObjectActivator<T>)lambda.Compile();
            return compiled;
        }

        private static BlockControl[] GenerateBlockControlInstancesForClassesDerivedFromTypeFaster(Type blockOperationInterface)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(assemblyType => blockOperationInterface.IsAssignableFrom(assemblyType))
                .Where(assemblyType => assemblyType.IsClass)
                .Where(assemblyType => assemblyType != typeof(DefaultBlockDiagramOperation))
                .Where(assemblyType => assemblyType != typeof(BaseBlockDiagramOperation))
                .Select(operationType =>
                {
                    Type genericBlockType = typeof(Block<>).MakeGenericType(operationType);
                    object? operationTypeInstance = Activator.CreateInstance(operationType);
                    if (operationTypeInstance is null) return null;

                    ConstructorInfo ctor = genericBlockType.GetConstructors().First();
                    ObjectActivator<object> createdActivator = GetActivator<object>(ctor);

                    return createdActivator(operationTypeInstance)!;
                })
                .Select(genericBlockOperationInstance =>
                {
                    ConstructorInfo ctor = typeof(BlockControl).GetConstructors().First();
                    ObjectActivator<BlockControl> createdActivator = GetActivator<BlockControl>(ctor);

                    return createdActivator(genericBlockOperationInstance!);
                })
                .ToArray();
        }

        //private static object?[] GenerateBlockControlInstancesForClassesDerivedFromType(Type blockOperationInterface)
        //{
        //    if (blockOperationInterface is null) throw new ArgumentNullException(nameof(blockOperationInterface));

        //    return AppDomain.CurrentDomain.GetAssemblies()
        //        .SelectMany(assembly => assembly.GetTypes())
        //        .Where(assemblyType => blockOperationInterface.IsAssignableFrom(assemblyType))
        //        .Where(assemblyType => assemblyType.IsClass)
        //        .Where(assemblyType => assemblyType != typeof(DefaultBlockDiagramOperation))
        //        .Where(assemblyType => assemblyType != typeof(BaseBlockDiagramOperation))
        //        .Select(operationType =>
        //        {
        //            Type genericBlockType = typeof(Block<>).MakeGenericType(operationType);
        //            object? operationTypeInstance = Activator.CreateInstance(operationType);
        //            if (operationTypeInstance is null) return null;

        //            BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
        //            object? genericBlockInstance = Activator.CreateInstance(
        //                type: genericBlockType,
        //                bindingAttr: flags,
        //                binder: null,
        //                args: [operationTypeInstance],
        //                culture: CultureInfo.CurrentCulture);
        //            if (genericBlockInstance is null) return null;

        //            ((Block)genericBlockInstance).Title = "Block";

        //            return genericBlockInstance;
        //        })
        //        .Select(genericBlockOperationInstance =>
        //            Activator.CreateInstance(typeof(BlockControl), genericBlockOperationInstance))
        //        .ToArray();
        //}
    }
}
