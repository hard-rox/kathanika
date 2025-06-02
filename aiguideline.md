### 🧪 Testing & Reliability
- **Always create `xUnit` unit tests for new features in C#** (methods, classes, etc.).
- **Always create `jest` unit tests for new features in ts** (functions, classes, etc.).
- **After updating any logic**, check whether existing unit tests need to be updated. If so, do it.
- **Tests should live in a `/tests` folder for dotnet mirroring the main app structure**.
    - Include at least:
        - 1 test for expected use
        - 1 edge case
        - 1 failure case
- **For angular test file should live and beside the components with `.spec.ts` extension already**.
- **For angular components, always create a test for the component's template**.
    - DO NOT use `jasmine`
    - DO NOT use `karma`
    - DO NOT create `.spec.ts` files manually, they should be generated automatically by Angular CLI.
    - DO NOT use `any` type in tests.
    - DO NOT test private methods & properties directly. Use html templates and public methods to verify behavior.
    - DO use `jest`
    - Always modify the generated `.spec.ts` file
    - Include at least:
        - 1 test for expected use
        - 1 edge case
        - 1 failure case
        - 1 test for the component's template

### 📚 Documentation & Explainability
- **Update `README.md`** when new features are added, dependencies change, or setup steps are modified.
- **Comment non-obvious code** and ensure everything is understandable to a mid-level developer.
- When writing complex logic, **add an inline `# Reason:` comment** explaining the why, not just the what.

### 🧠 AI Behavior Rules
- **Never assume missing context. Ask questions if uncertain.**
- **Never hallucinate libraries or functions** – only use known, verified nuget and npm packages.
- **Always confirm file paths and module names** exist before referencing them in code or tests.
- **Never delete or overwrite existing code** unless explicitly instructed to.