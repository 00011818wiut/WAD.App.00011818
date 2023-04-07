export function rawFormData(formData) {
    const object = {};
    formData.forEach((value, key) => (object[key] = value));

    return object
}