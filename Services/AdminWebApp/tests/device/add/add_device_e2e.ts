import test, { expect } from '@playwright/test';

test.describe.parallel('Add Device', () => {
	test('Add device to database', async ({ page }) => {
		await page.goto('/device/add');

		await page.selectOption('#version', '2');
		await page.fill('#ip_address', '1.1.1.1');
		await page.fill('#port', '161');
		await page.fill('#community', 'public');

		await page.click('#submit');

		await page.goto('/device');

		const deviceIp = await page.innerText('table > tbody > tr:last-child > td:nth-child(3)');
		expect(deviceIp).toBe('1.1.1.1');
	});
});
