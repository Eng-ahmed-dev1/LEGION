namespace FantasyFootball.Infrastructure.Configuration;

public class HangfireLogFilter : JobFilterAttribute, IElectStateFilter
{
    public void OnStateElection(ElectStateContext context)
    {
        if (context.CandidateState is FailedState failedState)
        {
            Log.Error(
                failedState.Exception,
                "Hangfire Job '{JobType}.{MethodName}' (ID: {JobId}) failed. (Failed Synchronization or Execution)",
                context.BackgroundJob.Job.Type.Name,
                context.BackgroundJob.Job.Method.Name,
                context.BackgroundJob.Id);
        }
        else if (context.CandidateState is EnqueuedState)
        {
            Log.Information(
                "Hangfire Job '{JobType}.{MethodName}' (ID: {JobId}) is queued.",
                context.BackgroundJob.Job.Type.Name,
                context.BackgroundJob.Job.Method.Name,
                context.BackgroundJob.Id);
        }
        else if (context.CandidateState is ProcessingState)
        {
            Log.Information(
                "Hangfire Job '{JobType}.{MethodName}' (ID: {JobId}) started executing.",
                context.BackgroundJob.Job.Type.Name,
                context.BackgroundJob.Job.Method.Name,
                context.BackgroundJob.Id);
        }
        else if (context.CandidateState is SucceededState)
        {
             Log.Information(
                "Hangfire Job '{JobType}.{MethodName}' (ID: {JobId}) succeeded.",
                context.BackgroundJob.Job.Type.Name,
                context.BackgroundJob.Job.Method.Name,
                context.BackgroundJob.Id);
        }
    }
}
