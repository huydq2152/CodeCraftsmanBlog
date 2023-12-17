import { Component, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
    templateUrl: './post-category.component.html',
    animations: [appModuleAnimation()],
})
export class PostCategoryComponent extends AppComponentBase {
    constructor(injector: Injector) {
        super(injector);
    }
}
