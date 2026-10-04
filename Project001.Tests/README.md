# Project001 tests

The test project contains two kinds of tests:

- `Unit` — service tests with repository mocks. These do not contact Firebase/Firestore.
- `Integration` — HTTP tests using `WebApplicationFactory`. They run the real ASP.NET Core routing, dependency injection, controllers and Razor views, while replacing the recipe service with a test double.

Run everything from the solution directory:

```bash
dotnet test
```

The integration tests intentionally do not require `firebase-service-account.json` or a live Firestore connection.
