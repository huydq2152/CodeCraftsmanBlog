export class SlugHelper {
    static generateSlug(code: string): string {
        code = code.normalize('NFD').replace(/[\u0300-\u036f]/g, '');
        return code.toLowerCase().replace(/\s+/g, '-');
    }
}
