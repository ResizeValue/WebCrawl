import VueRouter from 'vue-router'
import Results from './Pages/Results.vue'
import Details from './Pages/Details.vue'
import ErrorPage from './Pages/Error.vue'

export default new VueRouter({
    routes: [
        {
            path: '/',
            component: Results,
            name: 'resultsTable'
        },
        {
            path: '/Details/:id',
            component: Details,
            name: 'details'
        },
        {
            path: '/none',
            redirect: {
                name: 'resultsTable'
            }
        },
        {
            path: '*',
            component: ErrorPage
        }
    ],
    mode: 'history',
    scrollBehavior(to, from, savedPosition) {
        if (savedPosition) {
            return savedPosition
        }
        if (to.hash) {
            return { selector: to.hash }
        }
        return { x: 0, y: 0 }
    }
})