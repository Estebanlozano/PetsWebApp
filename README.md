# PetsWebApp

## Branching Strategy

The Pet Adoption Software project follows a branching strategy to manage the development process effectively. 

The strategy utilizes three types of branches: feature, bugfix, and hotfix.

### Feature Branches

Feature branches are created to develop new features or enhance existing ones. 

When working on a new feature, follow these steps:

Create a new branch from the develop branch with a descriptive name that reflects the feature you are working on. 

For example, **feature/pet-listing** or **feature/messaging-system**.

Implement the feature in the branch, making regular commits to track your progress.

Once the **feature is complete**, **create a pull request** to merge your branch into the develop branch.

**Request a code review** from your team members to ensure code quality and adherence to project standards.

Once the code review is approved, **merge** the feature branch into develop.

### Bugfix Branches

Bugfix branches are used to address bugs or issues discovered in the software. 

To fix a bug, follow these steps:

Create a new branch from the develop branch with a meaningful name that identifies the bug or issue you are addressing. 

For example, **bugfix/authentication-issue** or **bugfix/search-results**.

Apply the necessary fixes in the branch, committing your changes as appropriate.

Submit a pull request to merge the bugfix branch into the develop branch.

Request a code review to validate the bugfix.

Once the review is complete and any necessary changes are made, merge the bugfix branch into develop.

### Hotfix Branches

Hotfix branches are used to address critical issues in production. 

If a severe bug or security vulnerability is discovered, follow these steps:

Create a new branch from the main branch with a name that clearly identifies the hotfix. 

For example, **hotfix/security-vulnerability** or **hotfix/critical-bug-fix**.

Apply the necessary fixes directly to the hotfix branch.

Once the hotfix is ready, create a pull request to merge the hotfix branch into both main and develop branches.

Request an urgent code review and ensure the fix is thoroughly tested.

Once approved, merge the hotfix branch into main and develop.

Note: Hotfixes should be applied to both main and develop