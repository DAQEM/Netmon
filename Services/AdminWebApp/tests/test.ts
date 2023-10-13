import { expect, test } from '@playwright/test';

test('dashboard page has expected h1', async ({ page }) => {
	await page.goto('/dashboard');
	await expect(page.getByRole('heading', { name: 'Dashboard' })).toBeVisible();
});
