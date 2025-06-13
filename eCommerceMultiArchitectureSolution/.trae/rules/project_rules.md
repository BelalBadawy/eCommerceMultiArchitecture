# 🧠 Ultimate AI Rules for C# Developers

> Best Practices · Design Patterns · Performance · SOLID · KISS · DRY

---

## 🧩 1. Principles to Follow

### ✅ SOLID

- **S**ingle Responsibility: One class, one job.
- **O**pen/Closed: Extendable, not modifiable.
- **L**iskov Substitution: Subclasses should not break base class behavior.
- **I**nterface Segregation: Many small interfaces > one fat interface.
- **D**ependency Inversion: Depend on abstractions, not concretes.

### ✅ KISS – Keep It Simple, Stupid

- Prefer simple, clear logic over clever hacks.
- Avoid overengineering.

### ✅ DRY – Don’t Repeat Yourself

- Reuse code via methods, classes, or services.
- Extract reusable logic to shared components.

---

## 🧠 2. Architectural Guidelines

### ✅ Use Clean Architecture

- Divide into layers: `API`,`Shared`, `Domain`, `Application`, `Infrastructure`.
- Domain layer should be pure and independent.

### ✅ Use Dependency Injection

- Always use constructor injection.
- Use built-in DI (`Microsoft.Extensions.DependencyInjection`).

```csharp
public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    public OrderService(IOrderRepository repository)
    {
        _repository = repository;
    }
}
```

### ✅ Favor Composition Over Inheritance

## 🚀 4. Performance Best Practices

### ✅ Avoid unnecessary memory allocations

- Use `Span<T>` / `Memory<T>` for slicing.
- Use `StringBuilder` for string manipulation in loops.

### ✅ Use `async`/`await` for I/O-bound operations

```csharp
public async Task<IEnumerable<User>> GetUsersAsync()
{
    return await _userRepository.GetAllAsync();
}
```

### ✅ Minimize allocations in tight loops

- Avoid LINQ in hot paths. Prefer `for`/`foreach`.

### ✅ Cache where appropriate

- Use `IMemoryCache` or distributed cache (`IDistributedCache`).

### ✅ Pool expensive resources

- Use `ObjectPool<T>` when applicable.

---

## 🔍 5. Code Practices & Style

### ✅ Naming

- PascalCase for public types and methods.
- camelCase for private fields, parameters.
- `_underscorePrefix` for private readonly fields.

### ✅ Null Safety

- Use `nullable enable`.
- Use `??`, `?.`, and `??=` operators.

### ✅ Use Records for immutable models

```csharp
public record Customer(string Name, int Age);
```

### ✅ Use Expression-bodied members

```csharp
public int Square(int x) => x * x;
```

---

## 🧪 6. Testing

### ✅ Unit Test every business rule

- Use xUnit or NUnit.
- Avoid testing infrastructure in unit tests.

### ✅ Use Mocks for external dependencies

- Use Moq or NSubstitute.

```csharp
var mockRepo = new Mock<IOrderRepository>();
mockRepo.Setup(repo => repo.GetAll()).Returns(sampleData);
```

---

## 🔧 7. Tooling & Automation

### ✅ Code Analyzers

- Use Roslyn analyzers, SonarQube, or ReSharper.

### ✅ Formatters

- Use `.editorconfig` to enforce consistency.

### ✅ CI/CD Integration

- Integrate with GitHub Actions, Azure DevOps, etc.

---

## 🔄 8. Avoid Anti-Patterns

| Anti-Pattern                  | Instead Use                         |
| ----------------------------- | ----------------------------------- |
| God Object                    | SRP with focused services           |
| Magic Strings                 | Enums or constants                  |
| Hardcoded Configs             | `IConfiguration` or options pattern |
| Overusing static              | Use DI & interfaces                 |
| Business Logic in Controllers | Move to Service Layer               |

---

## 📁 9. Folder Structure Example

```
/src
  /MyApp
    /Controllers
    /Services
    /Repositories
    /Domain
    /DTOs
    /Infrastructure
/tests
  /MyApp.Tests
```

---

## 🔚 10. Golden Rule

> "Code is read more often than it is written. Write it for humans, not just compilers."
