import { AfterViewInit, Component, Injector, OnInit, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { PostCategoryDto, PostCategoryServiceProxy } from '@shared/service-proxies/service-proxies';
import { Table } from 'primeng/table';
import { Paginator } from 'primeng/paginator';
import { LazyLoadEvent } from 'primeng/api';
import { finalize } from 'rxjs';
import { CreateOrEditPostCategoryModalComponent } from './create-or-edit-post-category-modal.component';

@Component({
    templateUrl: './post-category.component.html',
    animations: [appModuleAnimation()],
})
export class PostCategoryComponent extends AppComponentBase implements AfterViewInit {
    @ViewChild('createOrEditPostCategoryModal', { static: true })
    createOrEditPostCategoryModal: CreateOrEditPostCategoryModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    //Filters
    filterText = '';

    constructor(injector: Injector, private _postCategoryService: PostCategoryServiceProxy) {
        super(injector);
    }
    ngAfterViewInit(): void {
        this.primengTableHelper.adjustScroll(this.dataTable);
    }

    getAll(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            if (this.primengTableHelper.records && this.primengTableHelper.records.length > 0) {
                return;
            }
        }
        this.primengTableHelper.showLoadingIndicator();

        this._postCategoryService
            .getAll(
                this.filterText,
                undefined,
                this.primengTableHelper.getSorting(this.dataTable),
                this.primengTableHelper.getSkipCount(this.paginator, event),
                this.primengTableHelper.getMaxResultCount(this.paginator, event)
            )
            .pipe(finalize(() => this.primengTableHelper.hideLoadingIndicator()))
            .subscribe((result) => {
                this.primengTableHelper.totalRecordsCount = result.totalCount;
                this.primengTableHelper.records = result.items;
                console.log('this.primengTableHelper.records = ', this.primengTableHelper.records);

                this.primengTableHelper.hideLoadingIndicator();
            });
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    createPostCategory(): void {
        this.createOrEditPostCategoryModal.show();
    }

    deletePostCategory(postCategory: PostCategoryDto): void {
        this.message.confirm(
            this.l('PostCategoryDeleteWarningMessage', postCategory.code),
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._postCategoryService.delete(postCategory.id).subscribe(() => {
                        this.reloadPage();
                        this.notify.success(this.l('SuccessfullyDeleted'));
                    });
                }
            }
        );
    }
}
