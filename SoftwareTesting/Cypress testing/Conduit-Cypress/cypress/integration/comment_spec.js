/// <reference types="Cypress"/>

describe('comments', ()=>{
    beforeEach(()=>{
        cy.task('cleanDatabase');
        cy.registerUserIfNeeded();
        cy.login();
    });

    const article = {
        title: 'My new article',
        description: 'About a topic',
        body: 'This is a new post',
        tagList: ['test']
    }

    it('Test post comments with stubbed response', () => {
        cy.writeArticleAndPostComments(article);
        cy.contains('[data-cy=comment]', 'great post!!').should('be.visible');
    });

    it('Test post comment waiting for server response', () => {
        cy.writeArticleAndPostComments(article);
        cy.postComment('my-new-article', 'my new comment');

        cy.visit('https://localhost:55476/article/my-new-article');
        cy.contains('[data-cy=comment]', 'my new comment').should('be.visible');
    });
});