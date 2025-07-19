# Commit Message Convention

This project follows the [Conventional Commits](https://www.conventionalcommits.org/) specification for commit messages. This leads to more readable messages that are easy to follow when looking through the project history.

## Commit Message Structure

```
<type>(<scope>): <short summary>
<BLANK LINE>
<body>
<BLANK LINE>
<footer>
```

### Type

Must be one of the following:

- **feat**: A new feature
- **fix**: A bug fix
- **docs**: Documentation only changes
- **style**: Changes that do not affect the meaning of the code (white-space, formatting, missing semi-colons, etc)
- **refactor**: A code change that neither fixes a bug nor adds a feature
- **perf**: A code change that improves performance
- **test**: Adding missing tests or correcting existing tests
- **build**: Changes that affect the build system or external dependencies
- **ci**: Changes to our CI configuration files and scripts
- **chore**: Other changes that don't modify src or test files
- **revert**: Reverts a previous commit

### Scope

The scope should be the name of the affected area of the project:

- **core**: Changes in core domain/application layer
- **web**: Changes in web interface
- **api**: Changes in API layer
- **db**: Database related changes
- **ui**: UI component changes
- **auth**: Authentication/Authorization changes
- **test**: Test infrastructure changes
- **deps**: Dependency updates

### Subject

The subject contains a succinct description of the change:

- Use the imperative, present tense: "change" not "changed" nor "changes"
- Don't capitalize the first letter
- No dot (.) at the end

### Body

The body should include the motivation for the change and contrast this with previous behavior:

- Use the imperative, present tense
- Include the motivation for the change and contrast with previous behavior

### Footer

The footer should contain any information about Breaking Changes and is also the place to reference GitHub issues that this commit closes:

- Breaking changes should start with `BREAKING CHANGE:`
- Reference issues at the bottom with `Fixes #123` or `Closes #123`

## Examples

### Feature with Breaking Change
```
feat(api): add ability to specify multiple search criteria

Added new parameters to the search endpoint allowing combination of multiple
search criteria. This enables more precise searching in the catalog.

BREAKING CHANGE: search endpoint now accepts an array of criteria instead
of single criterion.

Closes #123
```

### Bug Fix
```
fix(ui): resolve issue with pagination in vendor list

The pagination component was not updating when items per page changed.
Added proper event handling to refresh the list when pagination settings
are modified.

Fixes #456
```

### Documentation Update
```
docs(readme): update installation instructions

Added detailed steps for setting up the development environment including
prerequisites and troubleshooting guidelines.
```

### Style Changes
```
style(web): improve formatting in component templates

Applied consistent indentation and spacing in HTML templates to improve
code readability. No functional changes.
```

### Test Addition
```
test(core): add unit tests for order processing

Added comprehensive test suite for the order processing workflow including
edge cases and error scenarios.
```

## Git Hooks

We recommend using [Husky](https://github.com/typicode/husky) to enforce commit message conventions through Git hooks. The commit message pattern is enforced using commit-lint.

## Tools

To help with creating conventional commits, you can use:

1. VS Code Extension: "Conventional Commits" by vivaxy
2. IntelliJ Plugin: "Conventional Commit" by Edoardo Luppi
3. Command Line Tool: `commitizen`

## Additional Guidelines

1. Keep commits atomic: one logical change per commit
2. Write clear and meaningful commit messages
3. Reference issues and pull requests where appropriate
4. Include breaking changes prominently in the commit message
5. Use scope to provide context when necessary
