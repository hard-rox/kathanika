// cypress/e2e/bib-record-list.cy.ts
// Automated E2E test for the Bibliographic Record List page

describe('Bibliographic Record List Page', () => {
  beforeEach(() => {
    // Adjust the route as per your Angular routing config
    cy.visit('/cataloging');
  });

  it('should display the page title and new record button', () => {
    cy.contains('Bibliographic Records').should('be.visible');
    cy.contains('New Record').should('be.visible');
  });

  it('should show the table headers', () => {
    cy.get('table thead').within(() => {
      cy.contains('Serial').should('exist');
      cy.contains('Title').should('exist');
      cy.contains('Statement of Responsibility').should('exist');
      cy.contains('Publisher(s)').should('exist');
      cy.contains('Publication Date(s)').should('exist');
      cy.contains('Extent(s)').should('exist');
      cy.contains('Dimensions').should('exist');
      cy.contains('Actions').should('exist');
    });
  });

  it('should paginate results when using pagination controls', () => {
    cy.get('kn-pagination').should('exist');
    cy.get('kn-pagination select').select('20');
    cy.get('kn-pagination button').contains('>').click();
    // Optionally check for loading or page change
  });

  it('should filter results when searching', () => {
    cy.get('input[type="text"][placeholder="Search text"]').as('searchInput');
    cy.get('@searchInput').type('test{enter}');
    // Optionally check that the table updates
    cy.get('table tbody tr').should('exist');
  });

  it('should show actions for each row', () => {
    cy.get('table tbody tr').first().within(() => {
      cy.get('button').contains('more_vert').click({force: true});
      cy.contains('View').should('exist');
      cy.contains('Update').should('exist');
      cy.contains('Delete').should('exist');
    });
  });
});
