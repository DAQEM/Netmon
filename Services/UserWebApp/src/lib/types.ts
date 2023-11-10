enum AuthProtocol {
    SHA256 = 0,
    SHA384 = 1,
    SHA512 = 2,
}

enum PrivacyProtocol {
    AES = 0,
    AES192 = 1,
    AES256 = 2,
}

interface Device {
    id: string;
    name: string;
    ip_address: string;
    location: string;
    contact: string;
}

export type { Device, AuthProtocol, PrivacyProtocol }