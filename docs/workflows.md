# Kathanika Workflow Diagrams

This document contains the key business workflow diagrams for the Kathanika Library Management System.

## Core Workflows

### Book Acquisition Flow
```mermaid
flowchart TD
    A[Start] --> B[Create Purchase Order]
    B --> C{Approval Required?}
    C -->|Yes| D[Send for Approval]
    C -->|No| E[Process Order]
    D -->|Approved| E
    D -->|Rejected| F[Return to Draft]
    F --> B
    E --> G[Receive Items]
    G --> H[Catalog Items]
    H --> I[Add to Library Collection]
    I --> J[End]
```

### Circulation Flow
```mermaid
flowchart TD
    A[Member Requests Book] --> B{Is Book Available?}
    B -->|Yes| C[Check Member Status]
    B -->|No| D[Add to Wait List]
    C -->|Active| E[Issue Book]
    C -->|Inactive| F[Show Error]
    E --> G[Update Inventory]
    G --> H[Set Due Date]
    D --> I[Notify When Available]
    I --> B
    H --> J[Send Issue Receipt]
```

### Cataloging Flow
```mermaid
sequenceDiagram
    participant L as Librarian
    participant S as System
    participant M as MARC21
    participant DB as Database
    
    L->>S: Initialize Cataloging
    S->>L: Show Cataloging Form
    L->>S: Input Bibliographic Data
    S->>M: Validate MARC21 Format
    M-->>S: Validation Result
    alt is valid
        S->>DB: Store Record
        DB-->>S: Confirmation
        S->>L: Success Message
    else is invalid
        S->>L: Show Validation Errors
    end
```

### Member Management Flow
```mermaid
stateDiagram-v2
    [*] --> Registration
    Registration --> PendingApproval
    PendingApproval --> Active: Approved
    PendingApproval --> Rejected: Rejected
    Active --> Suspended: Violation
    Active --> Expired: Membership Period End
    Suspended --> Active: Restored
    Expired --> Active: Renewed
    Active --> [*]: Account Closed
```

## Note
These diagrams represent the high-level business workflows in Kathanika Library Management System. The actual implementation might have additional steps and error handling mechanisms. For technical architecture and development workflows, please refer to:
- System Architecture: [architecture.md](architecture.md)
- Development Guidelines: [CONTRIBUTING.md](../CONTRIBUTING.md)
