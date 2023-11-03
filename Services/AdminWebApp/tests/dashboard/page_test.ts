import test, { expect } from '@playwright/test';

test.describe.parallel('Dashboard page', () => {
    test('Has dashboard header', async ({ page }) => {
        await page.goto('/dashboard');

        const header = await page.innerText('h1');
        expect(header).toBe('Dashboard');
    });

    test('Has device table', async ({ page }) => {
        await page.goto('/dashboard');

        const table = await page.innerText('#device-table');
        expect(table).not.toBe('');
    });

    test('Has user table', async ({ page }) => {
        await page.goto('/dashboard');

        const table = await page.innerText('#user-table');
        expect(table).not.toBe('');
    });
});
