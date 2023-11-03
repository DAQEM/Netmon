import { test, expect } from '@playwright/test';

test.describe('Dashboard page', () => {
    test('Dashboard page has dashboard header', async ({ page }) => {
        await page.goto('/dashboard');

        const header = await page.innerText('h1');
        expect(header).toBe('Dashboard');
    });

    test('Dashboard page has device table', async ({ page }) => {
        await page.goto('/dashboard');

        const table = await page.innerText('#device-table');
        expect(table).not.toBe('');
    });

    test('Dashboard page has user table', async ({ page }) => {
        await page.goto('/dashboard');

        const table = await page.innerText('#user-table');
        expect(table).not.toBe('');
    });
});
