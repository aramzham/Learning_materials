/// <reference types="Cypress"/>

describe('New post on Conduit', () => {
    // adding a hook that runs before the test
    beforeEach(()=>{
        cy.task('cleanDatabase');
        cy.registerUserIfNeeded();
        cy.login();

        // aliases
        cy.get('[data-cy=new-post]').click().as('ClickOnNewPost');
        cy.get('[data-cy=title]').as('Title');
        cy.get('[data-cy=about]').as('About');
        cy.get('[data-cy=tags]').as('Tags');
        cy.get('[data-cy=article]').as('Article');
        cy.get('[data-cy=publish]').as('Publish');
        // do not do this
        // cy.get('[data-cy=publish]').click().as('ClickPublish')
    });

    it('write a new post', () => {
        cy.get('@ClickOnNewPost').click();
        cy.get('@Title').type('My New Post');
        cy.get('@About').type('This is my new post, and I am quite excited about it');
        cy.get('@Article').type('a lengthy story about Old Mooray...');
        cy.get('@Tags').type('test{enter}'); // we'll a new line
        cy.get('@Publish').click();

        // assert
        cy.location('pathname').should('equal', '/article/my-new-post');
    });

    it('edit existing post', () => {
        cy.get('@ClickOnNewPost').click();
        cy.get('@Title').type('My New Post');
        cy.get('@About').type('This is my new post, and I am quite excited about it');
        cy.get('@Article').type('a lengthy story about Old Mooray...');
        cy.get('@Tags').type('test{enter}'); // we'll a new line
        cy.get('@Publish').click();

        // assert
        cy.location('pathname').should('equal', '/article/my-new-post');

        cy.get('[data-cy=edit-article]').click();
        cy.location('pathname').should('equal', '/editor/my-new-post');

        cy.get('@Title').clear().type('My edited title');
        cy.get('@Tags').clear().type('myNewTag{enter}');
        cy.get('@Publish').click();
    });

    it('mark article as favorite', () => {
        cy.get('@ClickOnNewPost').click();
        cy.get('@Title').type('My New Post');
        cy.get('@About').type('This is my new post, and I am quite excited about it');
        cy.get('@Article').type('a lengthy story about Old Mooray...');
        cy.get('@Tags').type('test{enter}'); // we'll a new line
        cy.get('@Publish').click();

        // assert
        cy.location('pathname').should('equal', '/article/my-new-post');

        cy.get('[data-cy=profile]').click();
        cy.location('pathname').should('equal', '/@testuser');

        // favorite the article
        cy.get('.article-preview').should('have.length', 1).first().find('[data-cy=fav-article]').click();

        // validate that it's in fav articles tab
        cy.get('[data-cy=favorited-articles]').click();
        cy.contains('.article-preview', 'My New Post');
    });
});