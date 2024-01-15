import { expect, test } from '@playwright/test';

test('log user in', async ({ page }) => {
	await page.goto("/login")

	await page.fill('input[name="username"]', 'demo');
	await page.fill('input[name="password"]', 'P@ssw0rd!');

	await page.click('button[type="submit"]');

	expect(page.url()).toBe('http://localhost:4173/');
});
