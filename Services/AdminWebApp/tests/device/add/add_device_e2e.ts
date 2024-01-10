import test, { expect } from '@playwright/test';

test.describe.serial('Add Device', () => {
	const ipAddress = '1.1.1.1';

	test('Add device to database', async ({ page }) => {
		await page.goto('/device/add');

		await page.selectOption('#version', '2');
		await page.fill('#ip_address', ipAddress);
		await page.fill('#port', '161');
		await page.fill('#community', 'public');

		await page.click('#submit');

		const success = await page.innerText('#success-message');
		expect(success).toBe('Device added successfully.');
	});

	test('Device should be added to table', async ({ page }) => {
		await page.goto('/device');

		const device = await page.innerText(`#${ipAddress.replace(/\./g, '-')} > td:nth-child(3)`);
		expect(device).toBe(ipAddress);
	});
});