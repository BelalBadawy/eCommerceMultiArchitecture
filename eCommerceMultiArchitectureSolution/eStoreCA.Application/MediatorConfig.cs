using Mediator;
using Microsoft.Extensions.DependencyInjection;

// Assembly and module attributes must come before all other elements except using clauses and extern alias declarations
[assembly: MediatorOptions(
    Namespace = "eStoreCA.Application", // Add a valid namespace
    ServiceLifetime = ServiceLifetime.Scoped // Provide a valid value for ServiceLifetime
                                             // Optional: EnableMessageDispatch = true,
                                             // Optional: ScanAssembliesForHandlers = true,
                                             // Optional: UseIRequestInterceptorsWithSender = true,
                                             // Optional: UseINotificationInterceptorsWithSender = true
)]

// Define the GeneratedCodeAccess enum if it is missing
public enum GeneratedCodeAccess
{
    Public,
    Internal
}