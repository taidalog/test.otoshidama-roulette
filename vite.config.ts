import { defineConfig, loadEnv } from 'vite'

// https://vitejs.dev/config/
export default defineConfig(({ mode }) => {
    const env = { ...process.env, ...loadEnv(mode, process.cwd()) };

    console.log(env.VITE_BASE_URL)

    return {
        base: env.VITE_BASE_URL,
        build: {
            outDir: 'docs',
            emptyOutDir: true
        },
        envDir: 'env',
        clearScreen: false,
        server: {
            port: 8080,
            watch: {
                ignored: [
                    "**/*.fs" // Don't watch F# files
                ]
            }
        }
    }
})